registrationModule.controller("InstructorsController", function ($scope, instructorRepository) {
    instructorRepository.get().then(function (instructors) { $scope.instructors = instructors; });
});