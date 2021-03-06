﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using HalKit;
using HalKit.Models.Request;
using HalKit.Models.Response;

namespace GogoKit.Clients
{
    public class SearchClient : ISearchClient
    {
        private readonly IHalClient _halClient;

        public SearchClient(IHalClient halClient)
        {
            _halClient = halClient;
        }

        public Task<PagedResource<SearchResult>> GetAsync(string query)
        {
            return GetAsync(query, new SearchResultRequest());
        }

        public Task<PagedResource<SearchResult>> GetAsync(string query, SearchResultRequest request)
        {
            return GetInternalAsync(query, request, _halClient.GetAsync<PagedResource<SearchResult>>);
        }

        public Task<IReadOnlyList<SearchResult>> GetAllAsync(string query)
        {
            return GetAllAsync(query, new SearchResultRequest());
        }

        public Task<IReadOnlyList<SearchResult>> GetAllAsync(string query, SearchResultRequest request)
        {
            return GetInternalAsync(query, request, _halClient.GetAllPagesAsync<SearchResult>);
        }

        private async Task<T> GetInternalAsync<T>(
            string query,
            SearchResultRequest request,
            Func<Link, IRequestParameters, Task<T>> getSearchResultsFunc)
        {
            Requires.ArgumentNotNull(query, nameof(query));
            Requires.ArgumentNotNull(request, nameof(request));
            Requires.ArgumentNotNull(getSearchResultsFunc, nameof(getSearchResultsFunc));

            var root = await _halClient.GetRootAsync().ConfigureAwait(_halClient);

            request.Parameters.Add("query", query);

            return await getSearchResultsFunc(root.SearchLink, request).ConfigureAwait(_halClient);
        }
    }
}