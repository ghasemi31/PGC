/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {

    //    config.toolbar = 'MyToolbar';

    //    config.toolbar_MyToolbar =
    //	[
    //		{ name: 'document', items: ['NewPage', 'Preview'] },
    //		{ name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
    //		{ name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
    //		{ name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'
    //                 , 'Iframe']
    //		},
    //                '/',
    //		{ name: 'styles', items: ['Styles', 'Format'] },
    //		{ name: 'basicstyles', items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
    //		{ name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
    //		{ name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
    //		{ name: 'tools', items: ['Maximize', '-', 'About'] }
    //	];

    // Define changes to default configuration here. For example:
    config.language = 'en';
    //config.uiColor = '#AADC6E';
    config.extraPlugins = 'lineheight';
    //config.enterMode = CKEDITOR.ENTER_BR;

    config.filebrowserImageBrowseUrl = CKEDITOR.basePath + "ImageBrowser.aspx";
    config.filebrowserImageWindowWidth = 780;
    config.filebrowserImageWindowHeight = 720;
    config.filebrowserBrowseUrl = CKEDITOR.basePath + "LinkBrowser.aspx";
    config.filebrowserWindowWidth = 500;
    config.filebrowserWindowHeight = 650;
    //config.line_height = "1em;1.1em;1.2em;1.3em;1.4em;1.5em" ;
};
