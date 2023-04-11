using System.Diagnostics;
using DAL;
using Microsoft.AspNetCore.Mvc;
using FullStackTechTest.Models.Home;
using FullStackTechTest.Models.Shared;
using Models;
using System.Text.Json;
using System.Configuration;

namespace FullStackTechTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IPersonSpecialityRepository _personSpecialityRepository;
    private readonly ISpecialityRepository _specialityRepository;

    public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository, IAddressRepository addressRepository, IPersonSpecialityRepository personSpecialityRepository, ISpecialityRepository specialityRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        _personSpecialityRepository = personSpecialityRepository;
        _specialityRepository = specialityRepository;
    }

    public async Task<IActionResult> Index()
    {
        var model = await IndexViewModel.CreateAsync(_personRepository);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, false, _personRepository, _addressRepository, _personSpecialityRepository, _specialityRepository);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, true, _personRepository, _addressRepository, _personSpecialityRepository, _specialityRepository);
        return View("Details", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] DetailsViewModel model)
    {
        await _personRepository.SaveAsync(model.Person);
        await _addressRepository.SaveAsync(model.Address);
        //Save the personSpeciality from SpecialityCheckBoxList
        //delete all current db rows for this person
        await _personSpecialityRepository.DeleteByPersonIdAsync(model.Person.Id);
        //if the model has checkedboxes insert those ones
        foreach(CheckBoxModel specialityCheckBox in model.SpecialityCheckBoxList)
        {
            if (specialityCheckBox.IsChecked)
            {
                await _personSpecialityRepository.InsertAsync(new PersonSpeciality { PersonId = model.Person.Id, SpecialityId = specialityCheckBox.Value });
            }
        }

        return RedirectToAction("Details", new { id = model.Person.Id });
    }


    [HttpPost]
    public async Task<IActionResult> Upload(List<IFormFile> files)
    {
        long size = files.Sum(f => f.Length);

        var filePaths = new List<string>();
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                //lets do validation for this file then
                if (formFile.ContentType == "application/json")
                {
                    JsonSerializerOptions jsonoptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};

                    var filePath = Path.GetTempFileName();
                    filePaths.Add(filePath);
                    using (var stream = formFile.OpenReadStream())
                    {
                        List<PersonWithAddress>? newPersonWithAddresses = null;
                        try
                        {
                            newPersonWithAddresses = JsonSerializer.Deserialize<List<PersonWithAddress>>(stream, jsonoptions);
                        }
                        catch (Exception ex)
                        {
                            return RedirectToAction("Error", "Home", new { errorMessage = ex.Message});
                        }
                        
                        if (newPersonWithAddresses != null )
                        {
                            await ImportPersons(newPersonWithAddresses);
                        }
                    }
                    return Redirect("Index");
                } else
                {
                    return BadRequest();
                }


            } else
            {
                return NoContent();
            }
        }
        

        return Ok(new { count = files.Count, size, filePaths });
    }


    /// <summary>
    /// Updates each newPersonWithAddresses person and all of their addresses
    /// </summary>
    /// <param name="newPersonWithAddresses"></param>
    public async Task ImportPersons(List<PersonWithAddress> newPersonWithAddresses)
    {
        //import each person
        if (newPersonWithAddresses != null)
        {
            foreach (var newPersonWithAddress in newPersonWithAddresses)
            {
                if (newPersonWithAddress.GMC.ToString().Length == 7)
                {
                    //Trim the firstname to less than 50 chars
                    if (newPersonWithAddress.FirstName.Length > 50) newPersonWithAddress.FirstName =  newPersonWithAddress.FirstName.Substring(0, 50);
                    //Trim the firstname to less than 50 chars
                    if (newPersonWithAddress.LastName.Length > 50) newPersonWithAddress.LastName = newPersonWithAddress.LastName.Substring(0, 50);

                    //check if the person exists by GMC number, the import data doesn't have personId
                    Person personCheck = await _personRepository.GetByGMCAsync(newPersonWithAddress.GMC);
                    if (personCheck.FirstName != null)
                    {
                        //update person (already exists)
                        //Validation done in the model attributes
                        newPersonWithAddress.Id = personCheck.Id;
                        //update the person
                        //this is seems like the best thing to do here as the user will probably expect this;
                        await _personRepository.SaveAsync(newPersonWithAddress);

                        //look through all the addresses this person has
                        //check to see if the address already exists
                        //if it exists update
                        //if it does not already exist then insert
                        if (newPersonWithAddress.address != null)
                        {
                            ImportAddresses(newPersonWithAddress);
                        }
                    }
                    else
                    {
                        //insert person
                        newPersonWithAddress.Id = await _personRepository.InsertAsync(newPersonWithAddress);
                        //insert the addresses
                        ImportAddresses(newPersonWithAddress);
                    }
                }
                //TODO: throw this back to the page to show them this one isnt imported
                    

            }
        }
    }
    /// <summary>
    /// Updates/Inserts addresses of the newPerson
    /// </summary>
    /// <param name="newPersonWithAddress"></param>
    public async void ImportAddresses(PersonWithAddress newPersonWithAddress)
    {
        //if it exists update
        //if it does not already exist then insert
        if (newPersonWithAddress.address != null)
        {
            // look through all the addresses this person has
            foreach (Address newAddress in newPersonWithAddress.address)
            {
                //trim city to 100 chars
                if (newAddress.City.Length > 100) newAddress.City = newAddress.City.Substring(0, 100);
                //trim postcode to 8 chars
                if (newAddress.Postcode.Length > 8) newAddress.Postcode = newAddress.Postcode.Substring(0, 8);
                //trim line1 to 200 chars
                if (newAddress.Line1.Length > 200) newAddress.Line1 = newAddress.Line1.Substring(0, 200);
                //check to see if the address already exists - use a fuzzy match on the postcode
                Address addressCheck = await _addressRepository.GetForPersonIdAsync(newPersonWithAddress.Id);
                if (addressCheck.Postcode != null) {
                    if (addressCheck.Postcode.ToLower().Replace(" ", String.Empty) == newAddress.Postcode.ToLower().Replace(" ", String.Empty))
                    {
                        ////update the address
                        newAddress.Id = addressCheck.Id;
                        await _addressRepository.SaveAsync(newAddress);
                    } else
                    {
                        //Insert this new address for this person
                        newAddress.PersonId = newPersonWithAddress.Id;
                        await _addressRepository.InsertAsync(newAddress);
                    }
                } else
                {
                    //Insert this new address for this person
                    newAddress.PersonId = newPersonWithAddress.Id;
                    await _addressRepository.InsertAsync(newAddress);
                }
            }
        }
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string errorMessage = "")
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier , ErrorMessage = errorMessage});
    }

    
}