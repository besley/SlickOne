
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
        var userAccount = $.parseJSON(cookie);
        return userAccount;
    }

    lsm.getWebLogonUserID = function () {
        var userAccount = getWebLogonUserCookie();
        var userID = userAccount.UserID;
        return userID;
    }

    lsm.getWebLogonCompanyID = function () {
        var userAccount = getWebLogonUserCookie();
        var companyID = userAccount.CompanyID;
        return companyID;
    }

    lsm.getWebLogonUserAccountType = function () {
        var userAccount = getWebLogonUserCookie();
        var accountType = userAccount.AccountType;
        return accountType;
    }

    //#endregion

    //lsm.getStorage = function (key) {
    //    localStorage.getItem(key);
    //}

    //lsm.saveStorage = function (key, item) {
    //    if (item !== null)
    //        localStorage.setItem(key, item)
    //}

    //lsm.deleteStorage = function (key) {
    //    localStorage.removeItem(key);
    //}

    //lsm.saveUserIdentity = function (user) {
    //    var item = JSON.stringify(user);
    //    if (item !== null && item !== '') {
    //        localStorage.setItem("slickflowuser", item);
    //    }
    //}

    //lsm.getUserIdentity = function () {
    //    var userStr = localStorage.getItem("slickflowuser");
    //    if (userStr !== null && userStr !== '')
    //        return JSON.parse(userStr);
    //    else
    //        return null;
    //}

    //lsm.removeUserIdentity = function () {
    //    localStorage.removeItem("slickflowuser");
    //}

    //lsm.saveUserAuthData = function (authData) {
    //    var item = JSON.stringify(authData);
    //    if (item !== null && item !== '') {
    //        localStorage.setItem("slickflowauth", item);
    //    }
    //}

    //lsm.getUserAuthData = function () {
    //    var authStr = localStorage.getItem("slickflowauth");
    //    if (authStr !== null && authStr !== '')
    //        return JSON.parse(authStr);
    //    else
    //        return null;
    //}

    //lsm.removeUserAuthData = function () {
    //    localStorage.removeItem("slickflowauth");
    //}

    //lsm.removeTempStorage = function () {
    //    lsm.removeUserIdentity();
    //    lsm.removeUserAuthData();
    //}

    //lsm.checkUserPermission = function (resourceCode) {
    //    var isPermitted = false;
    //    var resourceList = lsm.getUserAuthData();

    //    for (var i = 0; i < resourceList.length; i++) {
    //        if (resourceList[i].ResourceCode == resourceCode) {
    //            isPermitted = true;
    //            break;
    //        }
    //    }

    //    return isPermitted;
    //}

    return lsm;
})();


