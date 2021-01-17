using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.web.Services
{
    public class MoviemindAPIService
    {
        private readonly HttpClient _httpClient;

        public MoviemindAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region Authentication
        public async Task<PostAuthenticateResponseModel> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel)
        {
            var postAuthenticateRequestModelJson = new StringContent(
                JsonSerializer.Serialize(postAuthenticateRequestModel),
                Encoding.UTF8,
                "application/json");

            using var httpResponse = await _httpClient.PostAsync("users/authenticate", postAuthenticateRequestModelJson);

            using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                // Get the Refresh Token details out of the response and save them in the model
                string refreshToken = httpResponse.Headers.GetValues("Set-Cookie").SingleOrDefault();

                PostAuthenticateResponseModel postAuthenticateResponseModel = await JsonSerializer.DeserializeAsync<PostAuthenticateResponseModel>(httpResponseStream);
                postAuthenticateResponseModel.RefreshToken = refreshToken;

                return postAuthenticateResponseModel;
            }

            throw await JsonSerializer.DeserializeAsync<MovieMindException>(httpResponseStream);
        }

        public async Task<PostAuthenticateResponseModel> RefreshToken()
        {
            using var httpResponse = await _httpClient.PostAsync("users/refresh-token", null);

            using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                // Get the Refresh Token details out of the response and save them in the model
                string refreshToken = httpResponse.Headers.GetValues("Set-Cookie").SingleOrDefault();

                PostAuthenticateResponseModel postAuthenticateResponseModel = await JsonSerializer.DeserializeAsync<PostAuthenticateResponseModel>(httpResponseStream);
                postAuthenticateResponseModel.RefreshToken = refreshToken;

                return postAuthenticateResponseModel;
            }

            throw await JsonSerializer.DeserializeAsync<MovieMindException>(httpResponseStream);
        }
        #endregion

        #region Generics

        public async Task<List<T>> GetModels<T>(string route)
        {
            var httpResponse = await _httpClient.GetAsync(route);

            if (httpResponse.IsSuccessStatusCode)
            {
                using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<List<T>>(httpResponseStream);
            }

            throw await HandleError(httpResponse, "GetModels");
        }

        public async Task<T> GetModel<T>(string id, string route)
        {
            var httpResponse = await _httpClient.GetAsync($"{route}/{id}");

            if (httpResponse.IsSuccessStatusCode)
            {
                using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<T>(httpResponseStream);
            }

            throw await HandleError(httpResponse, "GetModel");
        }

        public async Task<TypeOut> PostModel<TypeIn, TypeOut>(TypeIn model, string route)
        {
            var postModelJson = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            using var httpResponse = await _httpClient.PostAsync(route, postModelJson);

            if (httpResponse.IsSuccessStatusCode)
            {
                using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<TypeOut>(httpResponseStream);
            }

            throw await HandleError(httpResponse, "PostModel");
        }

        public async Task ResetPassword<TypeIn>(TypeIn model, string route)
        {
            var postModelJson = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            using var httpResponse = await _httpClient.PostAsync(route, postModelJson);

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    return;
                }
            }

            throw await HandleError(httpResponse, "PostModel");
        }

        public async Task PutModel<T>(string id, T model, string route)
        {
            var putModelJson = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            using var httpResponse = await _httpClient.PutAsync($"{route}/{id}", putModelJson);

            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            throw await HandleError(httpResponse, "PutModel");
        }

        public async Task DeleteModel(string id, string route)
        {
            using var httpResponse = await _httpClient.DeleteAsync($"{route}/{id}");

            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            throw await HandleError(httpResponse, "DeleteModel");
        }

        public async Task PatchModel<T>(string id, T model, string route)
        {
            var patchModelJson = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            using var httpResponse = await _httpClient.PatchAsync($"{route}/{id}", patchModelJson);

            if (httpResponse.IsSuccessStatusCode)
            {
                return;
            }

            throw await HandleError(httpResponse, "PatchModel");
        }

        #endregion

        private async Task<Exception> HandleError(HttpResponseMessage httpResponse, string method)
        {
            if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                throw new UnauthorizedException("You are not logged in", this.GetType().Name, method, "401");
            }
            else if (httpResponse.StatusCode.Equals(HttpStatusCode.Forbidden))
            {
                throw new ForbiddenException("Access denied", this.GetType().Name, method, "403");
            }
            else
            {
                using var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

                throw await JsonSerializer.DeserializeAsync<MovieMindException>(httpResponseStream);
            }
        }
    }
}
