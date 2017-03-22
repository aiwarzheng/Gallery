var app = angular.module("myGallery", ['ngSanitize']);
app.controller("GalleryController", ['$scope', '$http', '$compile', '$timeout', function ($scope, $http, $compile, $timeout) {
    $scope.currentPage = 1;
    $scope.currentType = 0;
    $scope.currentContent = 0;
    $scope.category = {
        current: 'all',
        items: ['all', 'house', 'car', 'animal', 'environment']
    };

    $scope.filterImage = function (category) {
        $scope.category.current = category;
        $scope.currentPage = 1;
        $scope.currentType = $scope.getCategoryIndex(category);
        $scope.loadImages();
    }

    $scope.getCategoryIndex = function (category) {
        switch (category) {
            case 'house':
                return 1;
            case 'car':
                return 2;
            case 'animal':
                return 3;
            case 'environment':
                return 4;
            default:
                return 0;
        }
    }

    $scope.setPreference = function (image, like) {
        $http.post("api/prefer", { imageId: image.ImageID, like: like }).then(function (response) {
            if (response.data.state === true) {
                image.LikeType = like == 0 ? 2 : 1;
            }
            else
            {
                alert("Submit failure");
            }
        });
    }

    $scope.changeContent = function (content) {
        $scope.currentContent = content;
        $scope.currentType = 0;
        $scope.currentPage = 1;
        $scope.loadImages();
    }

    $scope.loadImages = function () {
        $scope.loading = true;
        $http.post("api/Images", { content: $scope.currentContent, type: $scope.currentType, page: $scope.currentPage }).then(function (response) {
            $scope.loading = false;
            $scope.images = response.data;

            $scope.loadPageInfo($scope.images.Pages);
        });
    }

    $scope.loadPageInfo = function (pageInfo) {
        $http.post("/paginate", { total: pageInfo.TotalItems, itemPerPage: pageInfo.ItemPerPage, page: pageInfo.CurrentPage }).then(function (response) {
            $scope.paginateInfo = response.data;

            var html = $compile($scope.paginateInfo)($scope);

            angular.element(document.getElementById('image_page_info')).html(html);
        });
    }

    $scope.paginateImage = function (page) {
        $scope.currentPage = page;
        $scope.loadImages();
    }

    $scope.$watch("images", function (newValue, oldValue) {
        $timeout(function () {
            $('.image_list_bd').each(function () {
                $(this).magnificPopup({
                    delegate: '.image-popup-vertical-fit',
                    type: 'image',
                    closeOnContentClick: true,
                    mainClass: 'mfp-img-mobile',
                    image: {
                        verticalFit: true
                    },
                    titleSrc: function (item) {
                        return item.el.attr('title');
                    }
                });
            });
        });
    });

    $scope.loadImages();
}]);
