using AutoMapper;
using Discount.gRPC.Interfaces;
using Discount.gRPC.Models;
using Discount.gRPC.Protos;
using Grpc.Core;

namespace Discount.gRPC.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ICouponRepository _couponRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _couponRepository = couponRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _couponRepository.GetDiscount(request.ProductId) ??
                     throw new RpcException(new Status(StatusCode.NotFound, "Discount Not Fount"));
        _logger.LogInformation("Discount is retrieve for ProductName : {productName}, Amount: {amount}",
            coupon.ProductName, coupon.Amount);
        return _mapper.Map<CouponRequest>(coupon);
    }

    public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
    {
        var coupon = await _couponRepository.CreateDiscount(_mapper.Map<Coupon>(request));
        if (coupon is null)
            _logger.LogInformation("Discount create failed");
        _logger.LogInformation("Discount is successfully created. @{Coupon}", coupon);
        return _mapper.Map<CouponRequest>(coupon);
    }

    public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request);
        var isUpdated = await _couponRepository.UpdateDiscount(coupon);
        if (isUpdated)
            _logger.LogInformation("Discount is successfully updated. @{Product}", request);
        _logger.LogInformation("Discount update failed");
        return _mapper.Map<CouponRequest>(coupon);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);
        if (isDeleted)
            _logger.LogInformation("Discount is successfully deleted. @{ProductId}", request);
        _logger.LogInformation("Discount delete failed");
        return new DeleteDiscountResponse { Success = isDeleted };
    }
}