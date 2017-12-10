
//local storage maanger
var lsm = (function () {
    function lsm() {
    }

    //#region cookie method
    //设置Header, Name and value
    function setRequestHeader(xhr, headerName, value) {
        xhr.setRequestHeader(headerName, value);
    }

    //设置header
    function setHeader(xhr) {
        var cookie = xhr.getResponseHeader('Set-Cookie');
        var ticket = getCookie(".AuthCookie", cookie);
        if (ticket !== "") {
            alert(ticket);
            xhr.setRequestHeader('Authorization', ticket);
        }
    }



    //按名称读取Cookie值
    function getCookie(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    }

    //添加cookie
    function setCookie(c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        var c_domain = '.localhost';
        document.cookie = c_name + "=" + c_value + ";path=/" + ";domain=" + c_domain;
    }

    //删除cookie
    function removeCookie(c_name) {
        //当设置过期时间为负数时，会清除cookie
        setCookie(c_name, '', -7);
    }

    //读取QueryString里的Name
    function getUrlQueryStringByName(name) {
        var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    }

    function getWebLogonUserCookie() {
        var name = "SlickOneWebLogonUserDataCookie";
        var cookie = getCookie(name);
        if (cookie !== undefined) {
            var userAccount = $.parseJSON(cookie);
            return userAccount;
        } else {
            return null;
        }
    }

    lsm.getWebLogonUserID = function () {
        var userAccount = getWebLogonUserCookie();
        var userID = userAccount.UserID;
        return userID;
    }

    lsm.getWebLogonCompanyID = function () {
        var userAccount = getWebLogonUserCookie();
        if (userAccount !== null) {
            var companyID = userAccount.CompanyID;
            return companyID;
        } else {
            return "";
        }
    }

    lsm.getWebLogonUserAccountType = function () {
        var userAccount = getWebLogonUserCookie();
        var accountType = userAccount.AccountType;
        return accountType;
    }

    //#endregion
    return lsm;
})();


