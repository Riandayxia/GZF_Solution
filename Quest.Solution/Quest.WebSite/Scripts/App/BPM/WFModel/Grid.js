/*
用户组树
*/
Ext.define('BPM.WFModel.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_wfmodel_grid',
    stateId: 'bpm_wfmodel_grid',
    controllerName: 'WFDesign',
    isSelect: false,
    al: true,
    layout: 'fit',
    requires: [
       Ext.grid.column.Action
    ],
    //方法地址
    dataUrl: 'WFModel/GetAll',
    modelArray: ['Name', 'TableId', 'DBFieldName', 'Manager', 'InstanceManager', 'CreateUserID', 'DesignJSON', 'InstallDate', 'InstallUserID', 'Status', 'Desc', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '流程名称', dataIndex: 'Name', flex: 1 },
		{ text: '对应表', dataIndex: 'TableId', flex: 1 },
		{ text: '完成标识', dataIndex: 'DBFieldName', flex: 1 },
		{ text: '管理人员', dataIndex: 'Manager', flex: 1 },
		{ text: '实例管理人员', dataIndex: 'InstanceManager', flex: 1 },
		{ text: '创建人', dataIndex: 'CreateUserID', flex: 1 },
		{ text: '设计Json', dataIndex: 'DesignJSON', flex: 1, hidden: true },
		{ text: '安装日期', dataIndex: 'InstallDate', flex: 1 },
		{ text: '安装人员', dataIndex: 'InstallUserID', flex: 1 },
		{ text: '状态', dataIndex: 'Status', flex: 1 },
		{ text: '描述', dataIndex: 'Desc', flex: 1, hidden: true },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1, hidden: true },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1, hidden: true },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1, hidden: true },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, hidden: true }
    ]
});

