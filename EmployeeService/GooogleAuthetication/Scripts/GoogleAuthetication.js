/// <reference path='jquery-1.10.2.js' />

function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accesToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accesToken)
                IsUserRegistered(accesToken);
        }
    }
}

function IsUserRegistered(accesToken) {
    $.ajax({
        url: 'api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accesToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                localStorage.setItem('accessToken', accesToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = 'Data.html';
            }
            else {
                SignupExternally(accesToken, response.LoginProvider)
            }
        }

    });
}

function SignupExternally(accesToken, provider) {
    debugger;
    $.ajax({
        url: 'api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accesToken
        },
        success: function (response) {
            window.location.href = "/api/Account/ExternalLogin?provider="+provider+"&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A61748%2FLogin.html&state=e-mLmpT0kW9HyicmWb1RsEpcxDxdMXYbGlp-Df9-dts1";
        }

    });
}
