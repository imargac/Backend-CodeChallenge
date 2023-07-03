namespace Medication.Api.Extensions
{
    using Medication.Api.Endpoints;

    public static class EndpointsMapping
    {
        private static readonly string _medicationTag = "Medication";

        public static void MapApiEndpoints(this WebApplication app)
        {
            CreateMedicationEndpoint.Map(app, _medicationTag);
            DeleteMedicationEndpoint.Map(app, _medicationTag);
            GetListMedicationsEndpoint.Map(app, _medicationTag);
            GetMedicationEndpoint.Map(app, _medicationTag);
        }
    }
}
