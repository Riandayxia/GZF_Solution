/*
* 数据表编辑页面
*/
Ext.define('Compiler.DBColumn.Edit', {
    extend: 'Ext.user.NEdit',
    xtype: 'compiler_dbcolumn_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 1
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '对应表', name: 'TableId', xtype: 'hiddenfield' },
        { fieldLabel: '字段名称', name: 'Name', xtype: 'textfield', allowBlank: true,value:'zd1' },
        { fieldLabel: '字段文本', name: 'Text', xtype: 'textfield', allowBlank: false, value: '字段' },
        { fieldLabel: '字段类型', name: 'DBType', xtype: 'combobox', allowBlank: false, valueField: 'Value', displayField: 'Name', value: 'varchar' },
        { fieldLabel: '描述', name: 'Desc', xtype: 'textarea', colspan: 2, width: 600 },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield', },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ],
    // 初始化
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
        me.set_Listener();
    },
    // 设置事件
    set_Listener: function () {
        var me = this;
        var dbType = me.down('combobox[name="DBType"]');
        //创建数据源[数组数据源]
        var genderStore = Ext.create('Ext.data.Store', {
            fields: ['Name', 'Value'],
            data: [
                { Name: 'bigint', Value: 'bigint' },
                { Name: 'binary', Value: 'binary' },
                { Name: 'bit', Value: 'bit' },
                { Name: 'char', Value: 'char' },
                { Name: 'datetime', Value: 'datetime' },
                { Name: 'decimal', Value: 'decimal' },
                { Name: 'float', Value: 'float' },
                { Name: 'image', Value: 'image' },
                { Name: 'int', Value: 'int' },
                { Name: 'money', Value: 'money' },
                { Name: 'nchar', Value: 'nchar' },
                { Name: 'ntext', Value: 'ntext' },
                { Name: 'numeric', Value: 'numeric' },
                { Name: 'nvarchar', Value: 'nvarchar' },
                { Name: 'real', Value: 'real' },
                { Name: 'smalldatetime', Value: 'smalldatetime' },
                { Name: 'smallint', Value: 'smallint' },
                { Name: 'smallmoney', Value: 'smallmoney' },
                { Name: 'sql_variant', Value: 'sql_variant' },
                { Name: 'text', Value: 'text' },
                { Name: 'timestamp', Value: 'timestamp' },
                { Name: 'tinyint', Value: 'tinyint' },
                { Name: 'uniqueidentifier', Value: 'uniqueidentifier' },
                { Name: 'varbinary', Value: 'varbinary' },
                { Name: 'varchar', Value: 'varchar' }
            ]
        });

        dbType.store = genderStore;
    }
});