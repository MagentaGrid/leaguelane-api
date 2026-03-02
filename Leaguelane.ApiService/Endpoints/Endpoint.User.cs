using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class Endpoint
    {
        public static RouteGroupBuilder AddUserRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetUsers).WithName("users").Produces<object>(200);
            //.RequireAuthorization(policy =>
            //       policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id}", GetUser).WithName("user").Produces<object>(200)
                .RequireAuthorization(policy =>
                        policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPost("", CreateUser).WithName("user-create").Produces<object>(200)
                .AllowAnonymous();

            group.MapPut("{id}", UpdateUser).WithName("user-update").Produces<object>(200)
                .RequireAuthorization(policy =>
                        policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString(), UserRole.User.ToString()));

            group.MapPost("authenticate", AuthenticateUser).WithName("user-authenticate").Produces<object>(200)
                .AllowAnonymous();

            group.MapPost("forgot-password", ForgotPassword).WithName("user-forgot-password").Produces<object>(200);

            group.MapPost("reset-password", ResetPassword).WithName("user-reset-password").Produces<object>(200);

            group.MapPost("validate-reset-token", ValidateResetToken).WithName("user-validate-reset-token").Produces<object>(200);

            return group;
        }

        /// <summary>
        /// Get users.
        /// Returns a list of users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetUsers(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUsersQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get user by id.
        /// Returns details for a user by id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetUser(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetUserDetailsQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create user.
        /// Creates a new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreateUser(ISender sender, [FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateUserCommand(user), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Update user.
        /// Updates an existing user by id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateUser(ISender sender, int id, [FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateUserCommand(user, id), cancellationToken);
            return TypedResults.Ok(result);
        }
        /// <summary>
        /// Authenticate user.
        /// Authenticates a user and returns token.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> AuthenticateUser(ISender sender, [FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new AuthenticateUserQuery(user), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Forgot password.
        /// Initiates forgot password flow.
        /// </summary>
        /// <param name="userFeatureService"></param>
        /// <param name="forgotPassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> ForgotPassword([FromServices] IUserFeatureService userFeatureService, [FromBody] ForgotPasswordRequestDto forgotPassword, CancellationToken cancellationToken)
        {
            var result = await userFeatureService.ForgetPassword(forgotPassword, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Reset password.
        /// Resets a user's password using token.
        /// </summary>
        /// <param name="userFeatureService"></param>
        /// <param name="resetPassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> ResetPassword([FromServices] IUserFeatureService userFeatureService, [FromBody] ResetPasswordRequestDto resetPassword, CancellationToken cancellationToken)
        {
            var result = await userFeatureService.ResetPassword(resetPassword, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Validate reset token.
        /// Validates reset token provided in header.
        /// </summary>
        /// <param name="userFeatureService"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> ValidateResetToken([FromServices] IUserFeatureService userFeatureService, [FromHeader(Name = "X-Reset-Token")] string token, CancellationToken cancellationToken)
        {

            var result = await userFeatureService.ValidateResetPasswordToken(token, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
