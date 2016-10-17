
Ext.define('BPM.WFForm.dialogs.ChildrenGrid', {
    extend: 'Ext.user.NGrid',
    xtype: 'children_grid',
    isSelect: true,
    columns: [
        { text: '显示列', dataIndex: 'Id', flex: 1, minWidth: 0 },
        { text: '显示名称', dataIndex: 'UsersId', flex: 1, minWidth: 0 },
        { text: '编辑模式', dataIndex: 'ProjectId', flex: 1, minWidth: 0 },
        { text: '显示模式', dataIndex: 'UserName', flex: 1, minWidth: 0, },
        { text: '合计', dataIndex: 'ProjectId', flex: 1, minWidth: 0 },
        { text: '显示顺序', dataIndex: 'UserName', flex: 1, minWidth: 0, },
    ]
});

