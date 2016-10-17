/**-----------------------------------------------------------------
* @explanation:流程模型信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFModel.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_wfmodel_edit',
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
        { xtype: 'textfield', fieldLabel: '流程名称', name: 'Name'},
        { xtype: 'textfield', fieldLabel: '表名称', name: 'DBTableName'},
        { xtype: 'textfield', fieldLabel: '完成标识', name: 'DBFieldName'},
        { xtype: 'textfield', fieldLabel: '管理人员', name: 'Manager'},
        { xtype: 'textfield', fieldLabel: '实例管理人员', name: 'InstanceManager'},
        { xtype: 'hiddenfield', fieldLabel: '创建人', name: 'CreateUserID'},
        { xtype: 'textfield', fieldLabel: '设计Json', name: 'DesignJSON'},
        { xtype: 'datefield', fieldLabel: '安装日期', name: 'InstallDate', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'textfield', fieldLabel: '安装人员', name: 'InstallUserID'},
        { xtype: 'numberfield', fieldLabel: '状态', name: 'Status'},
        { xtype: 'textfield', fieldLabel: '描述', name: 'Desc'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
