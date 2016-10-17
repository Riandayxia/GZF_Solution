Ext.define('UX.Ueditor.UEPlugin', {
    //extend: 'Ext.form.field.TextArea',
    extend: 'Ext.Panel',
    alias: 'widget.ueplugin',
    //alternateClassName: 'Ext.form.UEditor',
    requires: [
        'UX.Ueditor.frame.config',
        'UX.Ueditor.frame.min',
        'UX.Ueditor.frame.lang.zh-cn.zh-cn',
        //'Scripts.Ueditor.Formdesign.leipi_formdesign_v4',
        'UX.Ueditor.customplugins'
    ],
    layout: "auto",
    html: '<div class="edui-height-auto" id="TTTeditor" type="text/plain"></div>',
    ueditorConfig: {
        autoHeightEnabled: true,
        autoFloatEnabled: true,
        initialFrameWidth: '100%',
        //toolbars: [[
        //    'fullscreen', 'source', '|', 'undo', 'redo', '|',
        //    'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|',
        //    'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',
        //    'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|',
        //    'directionalityltr', 'directionalityrtl', 'indent', '|',
        //    'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|',
        //    'link', 'unlink', 'anchor', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
        //    'simpleupload', 'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'gmap', 'insertframe', 'insertcode', 'webapp', 'pagebreak', 'template', 'background', '|',
        //    'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage', '|',
        //    'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
        //    'print', 'preview', 'searchreplace', 'help', 'drafts', '|', 'formtext', 'formadd'
        //]]
    },
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        if (!me.ue) {
            me.ue = UE.getEditor('TTTeditor', Ext.apply(me.ueditorConfig, {
                //initialFrameHeight: me.height || '100%',
                initialFrameHeight: '600',
            }));
            me.ue.ready(function () {
                me.UEditorIsReady = true;
            });

            //这块 组件的父容器关闭的时候 需要销毁编辑器 否则第二次渲染的时候会出问题 可根据具体布局调整
            var win = me.up('window');
            if (win && win.closeAction == "hide") {
                win.on('beforehide', function () {
                    me.onDestroy();
                });
            } else {
                var panel = me.up('panel');
                if (panel && panel.closeAction == "hide") {
                    panel.on('beforehide', function () {
                        me.onDestroy();
                    });
                }
            }
        } else {
            me.ue.setContent(me.getValue());
        }
    },
    setValue: function (value) {
        var me = this;
        if (!me.ue) {
            me.setRawValue(me.valueToRaw(value));
        } else {
            me.ue.ready(function () {
                me.ue.setContent(value);
            });
        }
        me.callParent(arguments);
        return me.mixins.field.setValue.call(me, value);
    },
    getRawValue: function () {
        var me = this;
        if (me.UEditorIsReady) {
            me.ue.sync(me.getInputId());
        }
        v = (me.inputEl ? me.inputEl.getValue() : Ext.value(me.rawValue, ''));
        me.rawValue = v;
        return v;
    },
    destroyUEditor: function () {
        var me = this;
        if (me.rendered) {
            try {
                me.ue.destroy();
                var dom = document.getElementById(me.id);
                if (dom) {
                    dom.parentNode.removeChild(dom);
                }
                me.ue = null;
            } catch (e) { }
        }
    },
    onDestroy: function () {
        var me = this;
        me.callParent();
        me.destroyUEditor();
    }
});