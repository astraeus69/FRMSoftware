window.localStorageFunctions = {
    saveToken: function (token) {
        localStorage.setItem('jwtToken', token);
    },
    getToken: function () {
        return localStorage.getItem('jwtToken');
    },
    removeToken: function () {
        localStorage.removeItem('jwtToken');
    }
};
