Ext.define("BPM.WFForm.Layout", {
    extend: 'Ext.Panel',
    fullscreen: true,
    xtype: 'wfform_layout',
    layout: "fit",
    //title: '自定义表单',
    requires: [
        'UX.UEditor.UEPlugin'
    ],
    formattributeJSON: { hasEditor: "0" },
    items: [{
        xtype: 'ueplugin',
        sid: 'ueplugin',
        height: '600',
    }],
    //监听器
    listeners: {
    },
    initComponent: function () {
        var me = this;
        var tbarData = [
            { name: '新建', iconCls: 'uicon_new', xtype: 'button', cmd: 'formadd', winUrl: 'BPM.WFForm.dialogs.Attribute', winWidth: 400 },
            { name: '打开', iconCls: 'uicon_open', xtype: 'button', cmd: 'formopen', winUrl: 'BPM.WFForm.dialogs.Open', winWidth: 800, winHeight: 600 },
            { name: '属性', iconCls: 'uicon_setting', xtype: 'button', cmd: 'formattribute', winUrl: 'BPM.WFForm.dialogs.Attribute', winWidth: 400 },
            { name: '文本框', iconCls: 'uicon_input', xtype: 'button', cmd: 'formtext', winUrl: 'BPM.WFForm.dialogs.TextBox', winWidth: 500 },
            { name: '文本域', iconCls: 'uicon_textarea', xtype: 'button', cmd: 'formtextarea', winUrl: 'BPM.WFForm.dialogs.Textarea', winWidth: 500 },
            { name: 'Html编辑器', iconCls: 'uicon_html', xtype: 'button', cmd: 'formhtml', winUrl: 'BPM.WFForm.dialogs.Html', winWidth: 400 },
            { name: '单选按钮组', iconCls: 'uicon_radio', xtype: 'button', cmd: 'formradio', winUrl: 'BPM.WFForm.dialogs.RadioButton', winWidth: 500 },
            { name: '复选按钮组', iconCls: 'uicon_checkbox', xtype: 'button', cmd: 'formcheckbox', winUrl: 'BPM.WFForm.dialogs.RadioButton', winWidth: 500 },
            { name: '隐藏域', iconCls: 'uicon_hidden', xtype: 'button', cmd: 'formhidden', winUrl: 'BPM.WFForm.dialogs.HiddenField', winWidth: 500 },
            { name: '下拉列表框', iconCls: 'uicon_select', xtype: 'button', cmd: 'formselect', winUrl: 'BPM.WFForm.dialogs.DropdownList', winWidth: 500 },
            { name: 'Label标签', iconCls: 'uicon_label', xtype: 'button', cmd: 'formlabel', winUrl: 'BPM.WFForm.dialogs.Label', winWidth: 500 },
            { name: '按钮', iconCls: 'uicon_button', xtype: 'button', cmd: 'formbutton', winUrl: 'BPM.WFForm.dialogs.Button', winWidth: 500 },
            { name: '下拉组合框', iconCls: 'uicon_combox', xtype: 'button', cmd: 'formcombox', winUrl: 'BPM.WFForm.dialogs.DropdownCombobox', winWidth: 700 },
            { name: '计算字段', iconCls: 'uicon_sum', xtype: 'button', cmd: 'formsumtext', winUrl: 'BPM.WFForm.dialogs.Compute', winWidth: 500 },
            { name: '组织机构选择框', iconCls: 'uicon_org', xtype: 'button', cmd: 'formorg', winUrl: 'BPM.WFForm.dialogs.Organization', winWidth: 500 },
            { name: '左右选择框', iconCls: 'uicon_selectlr', xtype: 'button', cmd: 'formdictionary', winUrl: 'BPM.WFForm.dialogs.LeftAndRight', winWidth: 650 },
            { name: '日期时间选择', iconCls: 'uicon_datetime', xtype: 'button', cmd: 'formdatetime', winUrl: 'BPM.WFForm.dialogs.Date', winWidth: 630 },
            { name: '附件上传', iconCls: 'uicon_attachment', xtype: 'button', cmd: 'formfiles', winUrl: 'BPM.WFForm.dialogs.Accessory', winWidth: 400 },
            { name: '子表', iconCls: 'uicon_subtable', xtype: 'button', cmd: 'formsubtable', winUrl: 'BPM.WFForm.dialogs.Children', winWidth: 1000 },
            { name: '数据表格', iconCls: 'uicon_database_info', xtype: 'button', cmd: 'formgrid', winUrl: 'BPM.WFForm.dialogs.DataTable', winWidth: 500 },
            { name: '保存当前表单', iconCls: 'uicon_save', xtype: 'button', cmd: 'formsave', winUrl: 'BPM.WFForm.dialogs.SaveAs', winWidth: 400 },
            { name: '表单另存为', iconCls: 'uicon_saveas', xtype: 'button', cmd: 'formsaveas', winUrl: 'BPM.WFForm.dialogs.SaveAs', winWidth: 400 },
            { name: '发布表单', iconCls: 'uicon_page_code', xtype: 'button', cmd: 'formcompile' }
        ];
        var tdata = [];
        Ext.Array.forEach(tbarData, function (item) {
            var td = {
                tooltip: item.name, iconCls: item.iconCls, xtype: item.xtype,
                handler: function (but) {
                    me.fireEvent('execCommand', but, me, item);
                    var myUe = me.down('ueplugin').ue;
                    myUe.execCommand(item.cmd, '', myUe);
                }
            };
            tdata.push(td);
        })

        me.tbar = Ext.create("Ext.toolbar.Toolbar", {
            border: 1,
            style: {
                'background-color': '#FEFEFE'
            },
            width: '100%',
            items: tdata,
            listeners: {
                tap: function (but, e, eOpts) {
                    alert('Toolbar')
                }
            }
        });
        //UE.getEditor('ueplugin', { wordCount: false, maximumWords: 1000000000, autoHeightEnabled: false });
        this.callParent(arguments);
    },
});