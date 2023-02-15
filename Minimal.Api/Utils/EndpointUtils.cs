using Actions.Commands;
using Actions.Queries;
using Actions;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace Minimal.Api.Utils
{
    public static class EndpointUtils
    {
        public static IEndpointRouteBuilder QueryList<TQuery, TEntity, TRequest>(this IEndpointRouteBuilder routeBuilder, string route)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            routeBuilder.MapQueryGet(route, EndpointUtils.ForQueryGet<TQuery, TEntity, TRequest>);
            return routeBuilder;
        }

        private static IEndpointRouteBuilder MapQueryGet(this IEndpointRouteBuilder routeBuilder, string route, Delegate handler)
        {
            routeBuilder.MapGet(route, handler).AddGlobalFilters();
            return routeBuilder;
        }

        private static async Task<IResult> ForQueryGet<TQuery, TEntity, TRequest>(QueryFactory queryFactory)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            var query = queryFactory.Create<TQuery>();
            return TypedResults.Ok(await query.Execute());
        }

        public static IEndpointRouteBuilder QuerySingle<TQuery, TEntity, TRequest>(this IEndpointRouteBuilder routeBuilder, string route)
            where TQuery : QuerySingle<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            routeBuilder.MapQueryGetSingle(route, EndpointUtils.ForQueryGetSingle<TQuery, TEntity, TRequest>);
            return routeBuilder;
        }

        private static IEndpointRouteBuilder MapQueryGetSingle(this IEndpointRouteBuilder routeBuilder, string route, Delegate handler)
        {
            routeBuilder.MapGet(route, handler).AddGlobalFilters();
            return routeBuilder;
        }

        private static async Task<IResult> ForQueryGetSingle<TQuery, TEntity, TRequest>([AsParameters] TRequest request, QueryFactory queryFactory)
            where TQuery : QuerySingle<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            var query = queryFactory.Create<TQuery>();
            query.SetProps(request);
            return TypedResults.Ok(await query.Execute());
        }

        public static IEndpointRouteBuilder QueryPost<TQuery, TEntity, TRequest>(this IEndpointRouteBuilder routeBuilder, string route)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            routeBuilder.MapQueryPost(route, EndpointUtils.ForQueryPost<TQuery, TEntity, TRequest>);
            return routeBuilder;
        }

        private static IEndpointRouteBuilder MapQueryPost(this IEndpointRouteBuilder routeBuilder, string route, Delegate handler)
        {
            routeBuilder.MapPost(route, handler).AddGlobalFilters();
            return routeBuilder;
        }

        private static async Task<IResult> ForQueryPost<TQuery, TEntity, TRequest>([FromBody] TRequest request, QueryFactory queryFactory)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            var query = queryFactory.Create<TQuery>();
            query.SetProps(request);
            return TypedResults.Ok(await query.Execute());
        }

        public static IEndpointRouteBuilder CommandPost<TCommand, TEntity, TRequest>(this IEndpointRouteBuilder routeBuilder, string route)
            where TCommand : Command<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            routeBuilder.MapCommandPost(route, EndpointUtils.ForCommand<TCommand, TEntity, TRequest>);
            return routeBuilder;
        }


        public static IEndpointRouteBuilder MapCommandPost(this IEndpointRouteBuilder routeBuilder, string route, Delegate handler)
        {
            routeBuilder.MapPost(route, handler).AddGlobalFilters();
            return routeBuilder;
        }

        public static async Task<IResult> ForCommand<TCommand, TEntity, TRequest>([FromBody] TRequest request, CommandFactory commandFactory)
            where TCommand : Command<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : RequestBase
        {
            var command = commandFactory.Create<TCommand>();
            command.SetProps(request);

            await command.Execute();
            return TypedResults.Ok();
        }

        public static RouteHandlerBuilder AddGlobalFilters(this RouteHandlerBuilder builder)
        {
            return builder.AddEndpointFilter<ValidationFilter>();
        }
    }
}
