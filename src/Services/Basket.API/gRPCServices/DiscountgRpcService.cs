using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices;

public class DiscountgRpcService
{
    public readonly DiscountProtoService.DiscountProtoServiceClient _discountClient;

    public DiscountgRpcService(DiscountProtoService.DiscountProtoServiceClient discountClient)
    {
        _discountClient = discountClient;
    }

    public async Task<CouponRequest> GetDiscount(string productId)
    {
        var getDiscountData = new GetDiscountRequest { ProductId = productId };
        return await _discountClient.GetDiscountAsync(getDiscountData);
    }
}
