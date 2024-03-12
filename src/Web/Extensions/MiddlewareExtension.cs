using Web.Middlewares;

namespace Web.Extensions
{
	public static class MiddlewareExtension
	{
		public static IApplicationBuilder UseBasketTransfer(this IApplicationBuilder app)
		{
			return app.UseMiddleware<BasketTransferMiddleware>();
		}
	}
}
