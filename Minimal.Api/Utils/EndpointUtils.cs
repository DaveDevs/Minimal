using Actions.Commands;
using Actions.Queries;
using Actions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace Minimal.Api.Utils
{
    public static class EndpointUtils
    {
        public static async Task<IResult> ForCommand<TCommand, TEntity, TRequest>([FromBody] TRequest request, CommandFactory commandFactory)
            where TCommand : Command<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : UserRequestBase
        {
            var command = commandFactory.Create<TCommand>();
            command.Props = request;
            try
            {
                await command.Execute();
            }
            catch (ValidationException ex)
            {
                return TypedResults.BadRequest(ex.Errors);
            }
            return TypedResults.Ok();
        }

        public static async Task<IResult> ForQueryPost<TQuery, TEntity, TRequest>([FromBody] TRequest request, QueryFactory queryFactory)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : UserRequestBase
        {
            var query = queryFactory.Create<TQuery>();
            query.Props = request;
            return TypedResults.Ok(await query.Execute());
        }

        public static async Task<IResult> ForQueryGet<TQuery, TEntity, TRequest>(QueryFactory queryFactory)
            where TQuery : QueryList<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : UserRequestBase
        {
            var query = queryFactory.Create<TQuery>();
            return TypedResults.Ok(await query.Execute());
        }

        public static async Task<IResult> ForQueryGetSingle<TQuery, TEntity, TRequest>([AsParameters] TRequest request, QueryFactory queryFactory)
            where TQuery : Query<TEntity, TRequest>, new()
            where TEntity : Entity
            where TRequest : UserRequestBase
        {
            var query = queryFactory.Create<TQuery>();
            query.Props = request;
            return TypedResults.Ok(await query.Execute());
        }
    }
}
