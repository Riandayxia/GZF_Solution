
Ext.define('BPM.WFForm.dialogs.Open', {
    extend: 'Ext.user.NGrid',
    xtype: 'open_grid',
    controllerName: 'WFForm',
    isSelect: false,
    al: true,
    layout: 'fit',
    //方法地址
    dataUrl: 'WFForm/GetAll',
    modelArray: ['Title', 'DBTable', 'DBTableName', 'PKey', 'TitleField', 'Url_AddOrUpdate', 'RunHtml', 'DesignHtml', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '表单名称', dataIndex: 'Title', flex: 1 },
		{ text: '数据表', dataIndex: 'DBTable', flex: 1, hidden: true },
		{ text: '数据表名称', dataIndex: 'DBTableName', flex: 1 },
		{ text: '主键', dataIndex: 'PKey', flex: 1, hidden: true },
		{ text: '标题字段', dataIndex: 'TitleField', flex: 1 },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '处理地址', dataIndex: 'Url_AddOrUpdate', flex: 1, hidden: true },
		{ text: '表单DesignHtml', dataIndex: 'DesignHtml', flex: 1, hidden: true },
		{ text: '表单RunHtml', dataIndex: 'RunHtml', flex: 1, hidden: true },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1, hidden: true },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1, hidden: true },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, hidden: true }
    ]
});

