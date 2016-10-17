/**-----------------------------------------------------------------
* @explanation:自定义列表信息列表
* @created：rainday
* @create time：2016-09-02 13:16
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.CDList.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_cdlist_grid',
    stateId: 'bpm_cdlist_grid',
    controllerName: 'CDList',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'CDList/GetAll',
    modelArray: ['DBTableId', 'DBTableName', 'Creator', 'DesignJson', 'RunJson', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '数据库表Id', dataIndex: 'DBTableId', flex: 1, hidden: true },
		{ text: '数据库表', dataIndex: 'DBTableName', flex: 1 },
		{ text: '创建者', dataIndex: 'Creator', flex: 1 },
		{ text: 'DesignJson', dataIndex: 'DesignJson', flex: 1, hidden: true },
		{ text: 'RunJson', dataIndex: 'RunJson', flex: 1, hidden: true },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1, hidden: true },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1, hidden: true },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, hidden: true },

    ]
});
