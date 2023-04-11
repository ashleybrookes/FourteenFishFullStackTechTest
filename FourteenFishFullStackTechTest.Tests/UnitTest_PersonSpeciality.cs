using FullStackTechTest.Models.Shared;
using FullStackTechTest.Models.Home;

namespace FourteenFishFullStackTechTest.Tests
{
    public class UnitTest_PersonSpeciality
    {
        private Mock<IPersonSpecialityRepository> _personSpecialityRepositoryMock;
        private Mock<ISpecialityRepository> _specialityRepositoryMock;

        private int _personId = 1;

        /// <summary>
        /// Setting up the Mock respositories
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _personSpecialityRepositoryMock = new Mock<IPersonSpecialityRepository>();
            _specialityRepositoryMock = new Mock<ISpecialityRepository>();
        }
        /// <summary>
        /// Testing DetailsViewModel.CreateCheckBoxList - asserting not returning null , checkboxmodel count is the same as speciality count, with 2 rows personspeciality data asserting true Ischecked in for the correct CheckboxModel items
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestPersonSpecialitycheckBoxList_PersonWithSpecialityData()
        {
            Setup();

            _personId = 1;
            List<PersonSpeciality> personSpecialityList = new List<PersonSpeciality>()
            {
                new PersonSpeciality() { Id = 1, SpecialityId = 1, PersonId = _personId },
                new PersonSpeciality() { Id = 2, SpecialityId = 6, PersonId = _personId }
            };
            _personSpecialityRepositoryMock.Setup(x => x.ListByPersonIdAsync(_personId)).ReturnsAsync(personSpecialityList);

            List<Speciality> specialityList = new List<Speciality>()
            {
                new Speciality() { Id = 1, SpecialityName = "Anaesthetics" },
                new Speciality() { Id = 2, SpecialityName = "Neurology" },
                new Speciality() { Id = 3, SpecialityName = "Urology" },
                new Speciality() { Id = 4, SpecialityName = "Gynaecology" },
                new Speciality() { Id = 5, SpecialityName = "Pharma" },
                new Speciality() { Id = 6, SpecialityName = "Cardiology" }
            };
            _specialityRepositoryMock.Setup(x => x.ListAllAsync()).ReturnsAsync(specialityList);

            List<CheckBoxModel> result_specialityCheckBoxList = await DetailsViewModel.CreateCheckBoxList(_personId, _personSpecialityRepositoryMock.Object, _specialityRepositoryMock.Object);

            Assert.IsNotNull(result_specialityCheckBoxList);
            Assert.AreEqual(result_specialityCheckBoxList.Count, 6);
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 1 && x.Text == "Anaesthetics" && x.IsChecked == true));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 2 && x.Text == "Neurology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 3 && x.Text == "Urology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 4 && x.Text == "Gynaecology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 5 && x.Text == "Pharma" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 6 && x.Text == "Cardiology" && x.IsChecked == true));
        }

        /// <summary>
        /// Testing DetailsViewModel.CreateCheckBoxList - asserting not returning null , checkboxmodel count is the same as speciality count, with no personspeciality data asserting false Ischecked in all CheckboxModel
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestPersonSpecialitycheckBoxList_NoPersonSpecialityData()
        {
            
            Setup();

            _personId = 2;
            //no Data for personSpeciality
            List<PersonSpeciality> personSpecialityList = new List<PersonSpeciality>();

            //using a mock to set the result for the ListByPersonIdAsync in PersonSpecialityRepository
            _personSpecialityRepositoryMock.Setup(x => x.ListByPersonIdAsync(_personId)).ReturnsAsync(personSpecialityList);

            //the full list of 
            List<Speciality> specialityList = new List<Speciality>()
            {
                new Speciality() { Id = 1, SpecialityName = "Anaesthetics" },
                new Speciality() { Id = 2, SpecialityName = "Neurology" },
                new Speciality() { Id = 3, SpecialityName = "Urology" },
                new Speciality() { Id = 4, SpecialityName = "Gynaecology" },
                new Speciality() { Id = 5, SpecialityName = "Pharma" },
                new Speciality() { Id = 6, SpecialityName = "Cardiology" }
            };
            //using a mock to set the result (data) from the ListAllAsync method in SpecialityRepository
            _specialityRepositoryMock.Setup(x => x.ListAllAsync()).ReturnsAsync(specialityList);

            List<CheckBoxModel> result_specialityCheckBoxList = await DetailsViewModel.CreateCheckBoxList(_personId, _personSpecialityRepositoryMock.Object, _specialityRepositoryMock.Object);

            //The specialityCheckBoxlist should not be null
            Assert.IsNotNull(result_specialityCheckBoxList);
            //The specialityCheckBoxlist should have 6 items 
            Assert.AreEqual(result_specialityCheckBoxList.Count, 6);
            //The specialityCheckBoxlist Value is the Id in the database , this should match up with the name, they should all be false
            //They should all be false
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 1 && x.Text == "Anaesthetics" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 2 && x.Text == "Neurology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 3 && x.Text == "Urology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 4 && x.Text == "Gynaecology" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 5 && x.Text == "Pharma" && x.IsChecked == false));
            Assert.IsTrue(result_specialityCheckBoxList.Any(x => x.Value == 6 && x.Text == "Cardiology" && x.IsChecked == false));
        }
    }
}