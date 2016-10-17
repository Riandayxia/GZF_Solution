/**-----------------------------------------------------------------
* @explanation:表单管理信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFForm.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_wfform_edit',
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
        { xtype: 'textfield', fieldLabel: '表单名称', name: 'Title'},
        { xtype: 'textfield', fieldLabel: '数据表', name: 'DBTable'},
        { xtype: 'textfield', fieldLabel: '数据表名称', name: 'DBTableName'},
        { xtype: 'textfield', fieldLabel: '处理地址', name: 'Url_AddOrUpdate'},
        { xtype: 'textfield', fieldLabel: '标题字段', name: 'TitleField'},
        { xtype: 'textfield', fieldLabel: '运行html', name: 'RunHtml'},
        { xtype: 'textfield', fieldLabel: '设计html', name: 'DesignHtml'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
