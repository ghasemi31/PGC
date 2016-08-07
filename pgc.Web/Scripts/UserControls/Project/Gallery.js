$(document).ready(function () {
    $('.fancybox-thumbs').fancybox({
        prevEffect: 'none',
        nextEffect: 'none',
        closeBtn: true,
        nextClick: true,

        helpers: {
            thumbs: {
                width: 75,
                height: 50
            },
            title: {
                type: 'inside',
                position: 'bottom',
            },
        },
        afterLoad: function () {
            this.title = '' + (this.index + 1) + ' از ' + this.group.length + (this.title ? ' - ' + this.title : '');
        },
    });
})