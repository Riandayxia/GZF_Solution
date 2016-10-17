/**-----------------------------------------------------------------
* @explanation:流程实例信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFRunInstance.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_wfruninstance_edit',
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
        { xtype: 'textfield', fieldLabel: '实体唯一标识', name: 'MainId'},
        { xtype: 'hiddenfield', fieldLabel: '创建用户', name: 'UserId'},
        { xtype: 'textfield', fieldLabel: '运行时流程', name: 'DesignJSON'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
