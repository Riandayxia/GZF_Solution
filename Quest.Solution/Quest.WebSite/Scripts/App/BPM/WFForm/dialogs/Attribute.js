/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Attribute", {
    extend: 'Ext.user.NEdit',
    xtype: 'wffd_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    formSubmit: false,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 1
    },
    defaults: {
        labelAlign: 'right',
        width: 360
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '表单名称', name: 'Title', xtype: 'textfield', value: '表单测试' },
        { fieldLabel: '绑定数据表', name: 'DBTable', xtype: 'ndiction', dataUrl: '/CDTable/GetCombox', dicKey: 'DBTableId' },
        { fieldLabel: '表单名称', name: 'DBTableName', xtype: 'textfield', readOnly: true },
        { fieldLabel: '主键', name: 'PKey', xtype: 'hiddenfield' },
        { fieldLabel: '标题字段', name: 'TitleField', xtype: 'ndiction' },
        { fieldLabel: '处理字段', name: 'Url_AddOrUpdate', xtype: 'textfield', width: 420 },
        { fieldLabel: 'DesignHtml', name: 'DesignHtml', xtype: 'hiddenfield' },
        { fieldLabel: 'RunHtml', name: 'RunHtml', xtype: 'hiddenfield' }
    ],
    // 初始化
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
        var tableBox = me.down('[name="DBTable"]');

        var titleBox = me.down('[name="TitleField"]');
        titleBox.store = Ext.create("Ext.data.Store", {
            fields: ["Text", "Value", "Tobject"],
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                },
                url: 'CDColumn/GetCombox'
            }
        });
        var tableName = me.down('[name="DBTableName"]');
        var urlText = me.down('[name="Url_AddOrUpdate"]');
        // 字段数据表注册下来事件
        tableBox.addListener('select', function (combo, records, eOpts) {
            var tableId = combo.getTobject().Id;
            titleBox.store.load({
                params: { tId: tableId }
            });
            tableName.setValue(combo.getTobject().Name);
            urlText.setValue(combo.getTobject().Name+'/Add');
        });
    }
});