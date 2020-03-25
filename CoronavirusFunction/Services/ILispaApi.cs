using CoronavirusFunction.Models;
using Refit;
using System.Threading.Tasks;

namespace CoronavirusFunction.Services
{
    public interface ILispaApi
    {
        [Get("/query?where=1%3D1&objectIds=&time=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&resultType=none&distance=0.0&units=esriSRUnit_Meter&returnGeodetic=false&outFields=FID%2C+COD_ISTATN%2C+NOME_COM%2C+NOME_PRO%2C+POSITIVI%2C+DECEDUTI%2C+TOTALE&returnGeometry=false&returnCentroid=false&featureEncoding=esriDefault&multipatchOption=xyFootprint&maxAllowableOffset=&geometryPrecision=&outSR=&datumTransformation=&applyVCSProjection=false&returnIdsOnly=false&returnUniqueIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&returnQueryGeometry=false&returnDistinctValues=false&cacheHint=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&having=&resultOffset=&resultRecordCount=&returnZ=false&returnM=false&returnExceededLimitFeatures=true&quantizationParameters=&sqlFormat=none&f=pjson")]
        Task<LispaDataDto> GetLispaData();
    }
}
