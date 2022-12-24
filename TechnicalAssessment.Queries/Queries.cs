namespace TechnicalAssessment.Queries
{
    public class Queries
    {
        public static string SaveTransformation()
        {
            return "TechnicalAssessment/SaveTransformation";
        }

        public static string GetTransformation(int transformationId)
        {
            return $"TechnicalAssessment/GetTransformation/{transformationId}";
        }

        public static string GetTransformations(int? lineAreaId = null)
        {
            return $"TechnicalAssessment/GetTransformations/{lineAreaId}";
        }

        public static string DeleteAction(Guid actionId)
        {
            return $"TechnicalAssessment/DeleteAction/{actionId}";
        }

        public static string DeleteComponent(int componentId)
        {
            return $"TechnicalAssessment/DeleteComponent/{componentId}";
        }

        public static string DeleteTransformation(int transformationId)
        {
            return $"TechnicalAssessment/DeleteTransformation/{transformationId}";
        }
    }
}