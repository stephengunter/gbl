﻿using ApplicationCore.Settings;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Text;
using Newtonsoft.Json;
using ApplicationCore.Helpers;
using ApplicationCore.Exceptions;
using ApplicationCore.Views;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IRecaptchaService
    {
        Task<bool> VerifyAsync(string token, string ip = "");
    }

    public class GoogleRecaptchaService : IRecaptchaService
    {
       
        private readonly string _apiSecret;

        private readonly HttpClient _httpClient;

        public GoogleRecaptchaService(IHttpClientFactory clientFactory)
        {
            _apiSecret = Environment.GetEnvironmentVariable("RECAPTCHA_SECRET");
            if (String.IsNullOrEmpty(_apiSecret)) throw new EnvironmentVariableNotFound("RECAPTCHA_SECRET");

            _httpClient = clientFactory.CreateClient(Consts.Google);
        }

        public async Task<bool> VerifyAsync(string token, string ip = "")
        {
            var values = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", _apiSecret),
                new KeyValuePair<string, string>("response", token)
            };
            if (ip.HasValue()) values.Add(new KeyValuePair<string, string>("remoteip", ip));

            string action = "recaptcha/api/siteverify";
          
            try
            {
                var content = new FormUrlEncodedContent(values);


                var response = await _httpClient.PostAsync(action, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var recaptchaResult = JsonConvert.DeserializeObject<GoogleRecaptchaResponse>(result);

                    return recaptchaResult.Success;

                }
                else
                {
                    throw new RemoteApiException((int)response.StatusCode, $"{_httpClient.BaseAddress}/{action}");
                }
            }
            catch (Exception ex)
            {
                throw new RemoteApiException($"{_httpClient.BaseAddress}/{action}", ex);
            }
        }
    }
}
