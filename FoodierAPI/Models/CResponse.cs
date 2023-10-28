namespace FoodierAPI.Models
{
    public class CResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string ReturnData { get; set; }

        public CResponse()
        {
            ReturnCode = -1;
            ReturnMessage = string.Empty;
            ReturnData= string.Empty;
        }
        public CResponse(int returnCode, string returnMessage, string returnData = "") {
            ReturnCode = returnCode;
            ReturnMessage = returnMessage;
            ReturnData = returnData;
        }
    }
}
