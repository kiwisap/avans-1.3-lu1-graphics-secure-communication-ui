using Assets.Scripts.Models;
using Assets.Scripts.Models.Dto;
using Assets.Scripts.Utils;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Services
{
    public class AccountService : AbstractService
    {
        public async Task<ApiResult> RegisterAsync(RegisterDto registerDto)
        {
            string url = $"{BaseUrl}/account/register";
            var json = JsonUtils.Serialize(registerDto);

            using var request = CreateRequest(url, HttpMethod.Post, json);
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return ApiResult.Success();
            }
            else
            {
                var problemDetailError = request.downloadHandler.text;
                if (!string.IsNullOrWhiteSpace(problemDetailError))
                {
                    return ApiResult.Fail(JsonUtils.Deserialize<ProblemDetailError>(problemDetailError));
                }

                return ApiResult.Fail($"Registration failed: {request.error} - {request.downloadHandler.text}");
            }
        }

        public async Task<ApiResult> LoginAsync(string email, string password)
        {
            string url = $"{BaseUrl}/identity/login?useCookies=false&useSessionCookies=false";
            var requestData = new LoginDto { Email = email, Password = password };
            var json = JsonUtils.Serialize(requestData);

            using var request = CreateRequest(url, HttpMethod.Post, json);
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtils.Deserialize<AccessTokenResponseDto>(request.downloadHandler.text);

                Debug.Log($"Login successful. Token: {response.AccessToken}");
                PlayerPrefs.SetString("AccessToken", response.AccessToken);
                PlayerPrefs.Save();

                return ApiResult.Success();
            }
            else
            {
                var problemDetailError = request.downloadHandler.text;
                if (!string.IsNullOrWhiteSpace(problemDetailError))
                {
                    return ApiResult.Fail(JsonUtils.Deserialize<ProblemDetailError>(problemDetailError));
                }

                return ApiResult.Fail($"Login failed: {request.error} - {request.downloadHandler.text}");
            }
        }

        public async Task<ApiResult<UserDto>> GetCurrentUserAsync()
        {
            string url = $"{BaseUrl}/account/me";

            using var request = CreateRequest(url, HttpMethod.Get);
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtils.Deserialize<UserDto>(request.downloadHandler.text);
                return ApiResult<UserDto>.Success(response);
            }
            else
            {
                var problemDetailError = request.downloadHandler.text;
                if (!string.IsNullOrWhiteSpace(problemDetailError))
                {
                    return ApiResult<UserDto>.Fail(JsonUtils.Deserialize<ProblemDetailError>(problemDetailError));
                }

                return ApiResult<UserDto>.Fail($"Get current user failed: {request.error} - {request.downloadHandler.text}");
            }
        }

        public async Task<ApiResult<UserDto>> UpdateCurrentLevelAsync(int level)
        {
            string url = $"{BaseUrl}/account/current-level";
            var requestData = new ValueDto<int> { Value = level };
            var json = JsonUtils.Serialize(requestData);

            using var request = CreateRequest(url, HttpMethod.Post, json);
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtils.Deserialize<UserDto>(request.downloadHandler.text);
                // TODO update user data
                return ApiResult<UserDto>.Success(response);
            }
            else
            {
                var problemDetailError = request.downloadHandler.text;
                if (!string.IsNullOrWhiteSpace(problemDetailError))
                {
                    return ApiResult<UserDto>.Fail(JsonUtils.Deserialize<ProblemDetailError>(problemDetailError));
                }

                return ApiResult<UserDto>.Fail($"Update current level failed: {request.error} - {request.downloadHandler.text}");
            }
        }
    }
}
