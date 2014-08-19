registrationModule.factory('courseRepository', function ($http, $q) {
    return {
        get: function () {
            var deferred = $q.defer();
            $http.get('/Courses').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    }
});