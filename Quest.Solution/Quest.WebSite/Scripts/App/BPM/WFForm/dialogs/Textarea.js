/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Textarea", {
    extend: 'Ext.user.NEdit',
    xtype: 'textarea_edit',
    formSubmit: false,
    requires: [
        //'UX.Ueditor.frame.dialogs.internal'
    ],
    layout: {
        type: 'table',
        columns: 1
    },
    defaults: {
        labelAlign: 'right',
        width: 360
    },
    items: [
         { fieldLabel: '绑定字段', xtype: 'ndiction', name: 'BindFiled' },
         { fieldLabel: '值类型', xtype: 'hiddenfield', name: 'ValueType' },
         { fieldLabel: '默认值', xtype: 'textfield', name: 'Defaultvalue' },
         { fieldLabel: '宽度', name: 'TextWidth', xtype: 'textfield', value: '80%' },
         { fieldLabel: '高度', name: 'TextHeight', xtype: 'textfield', value: '90px' }
    ],
    // 初始化
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
        var filedBox = me.down('[name="BindFiled"]');
        var typeBox = me.down('[name="ValueType"]');
        var tableId = me.fromData.DBTable;
        filedBox.store = Ext.create("Ext.data.Store", {
            fields: ["Text", "Value", "Tobject"],
            autoLoad: true,
            proxy: {
                type: 'ajax',
                url: 'CDColumn/GetCombox?tId=' + tableId,
                reader: {
                    type: 'json',
                    root: 'data'
                }
            }
        });
    }
});