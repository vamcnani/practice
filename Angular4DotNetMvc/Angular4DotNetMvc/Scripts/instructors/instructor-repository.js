registrationModule.factory('instructorRepository', function ($http, $q) {
    return {
        get: function () {
            var deferred = $q.defer();
            $http.get('/Instructors').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    }
});