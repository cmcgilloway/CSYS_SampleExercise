using Newtonsoft.Json;
using SampleExercises.Services;
using SampleExercises.Models;

var dataSourcesDirectory = Path.Combine(Environment.CurrentDirectory, "DataSources");
var personsFilePath = Path.Combine(dataSourcesDirectory, "Persons_20220824_00.json");
var organizationsFilePath = Path.Combine(dataSourcesDirectory, "Organizations_20220824_00.json");
var vehiclesFilePath = Path.Combine(dataSourcesDirectory, "Vehicles_20220824_00.json");
var addressesFilePath = Path.Combine(dataSourcesDirectory, "Addresses_20220824_00.json");

//Quick test to ensure that all files are where they should be :)
foreach (var path in new[] { personsFilePath, organizationsFilePath, vehiclesFilePath, addressesFilePath })
{
    if (!File.Exists(path))
        throw new FileNotFoundException(path);
}

//TODO: Start your exercise here. Do not forget about answering Test #9 (Handled slightly different)
// Reminder: Collect the data from each file. Hint: You could use Newtonsoft's JsonConvert or Microsoft's JsonSerializer

DataAnalysisService dataAnalysisService = new DataAnalysisService(JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(personsFilePath)) ?? new List<Person>(),
    JsonConvert.DeserializeObject<List<Organization>>(File.ReadAllText(organizationsFilePath)) ?? new List<Organization>(),
    JsonConvert.DeserializeObject<List<Vehicle>>(File.ReadAllText(vehiclesFilePath)) ?? new List<Vehicle>(), 
    JsonConvert.DeserializeObject<List<Address>>(File.ReadAllText(addressesFilePath)) ?? new List<Address>());

//Test #1: Do all files have entities? (True / False)
Console.WriteLine(string.Format("Test #1 answer - Do all files have entities?  {0}", !dataAnalysisService.IsAnyDataEmpty()));

//Test #2: What is the total count for all entities?
Console.WriteLine(string.Format("Test #2 answer - Total Count of Entities:  {0}", dataAnalysisService.TotalEntities()));

//Test #3: What is the count for each type of Entity? Person, Organization, Vehicle, and Address
Console.WriteLine(string.Format("Test #3 answer - Count per Entity Type:  {0}", dataAnalysisService.EntityCounts()));

//Test #4: Provide a breakdown of entities which have associations in the following manor:
//         -Per Entity Count
//         - Total Count
Console.WriteLine(string.Format("Test #4 answer - Entities with Associations:  {0}", dataAnalysisService.EntitiesWithAssociations()));

//Test #5: Provide the vehicle detail that is associated to the address "4976 Penelope Via South Franztown, NH 71024"?
//         StreetAddress: "4976 Penelope Via"
//         City: "South Franztown"
//         State: "NH"
//         ZipCode: "71024"
List<Association> addressAssociations = dataAnalysisService.GetAssociations("Address", new Address
{
    StreetAddress = "4976 Penelope Via",
    City = "South Franztown",
    State = "NH",
    ZipCode = "71024"
});
Console.WriteLine(string.Format("Test #5 answer - Vehicle Assoicated with Address:  {0}", dataAnalysisService.GetAssociatedEntities(addressAssociations)));

//Test #6: What person(s) are associated to the organization "thiel and sons"?
List<Association> organizationAssociations = dataAnalysisService.GetAssociations("Organization", new Organization
{
    Name = "Thiel and Sons"
});
Console.WriteLine(string.Format("Test #6 answer - Person Associated with Organization:  {0}", dataAnalysisService.GetAssociatedEntities(organizationAssociations)));

//Test #7: How many people have the same first and middle name?
Console.WriteLine(string.Format("Test #7 answer - # Person Entries with Same Value for First/Middle Name:  {0}", dataAnalysisService.GetDuplicateNameCount()));

//Test #8: Provide a breakdown of entities where the EntityId contains "B3" in the following manor:
//         -Total count by type of Entity
//         - Total count of all entities
Console.WriteLine(string.Format("Test #8 answer - Entities with ID containing 'b3':  {0}", dataAnalysisService.FoundIdSearchCounts("b3")));

Console.WriteLine();
Console.WriteLine("Test Complete.  Press any key to close window.");
Console.ReadKey();