using AutoMapper;
using Discount.gRPC.Models;
using Discount.gRPC.Protos;

namespace Discount.gRPC.MappingProfile;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponRequest>().ReverseMap();
    }
}