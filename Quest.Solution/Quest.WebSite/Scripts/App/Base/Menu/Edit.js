/**-----------------------------------------------------------------
* @explanation:菜单信息编辑
* @created：rainday
* @create time：2016-08-19 09:34
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("Base.Menu.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'base_menu_edit',
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
        { xtype: 'textfield', fieldLabel: '菜单名称', name: 'Name' },
        { xtype: 'numberfield', fieldLabel: '菜单类型', name: 'MType' },
        { xtype: 'numberfield', fieldLabel: '菜单用途', name: 'Use' },
        { xtype: 'textfield', fieldLabel: '上级菜单名称', name: 'ParentName' },
        { xtype: 'hiddenfield', fieldLabel: '控制器名称', name: 'ControllerName' },
        { xtype: 'textfield', fieldLabel: '图标样式', name: 'IconClass' },
        { xtype: 'hiddenfield', fieldLabel: '功能名称', name: 'FeatureName' },
        { xtype: 'textfield', fieldLabel: '地址', name: 'MenuLink', width: 560 },
        { xtype: 'textareafield', fieldLabel: '参数', name: 'Params', width: 560 },
        { xtype: 'hiddenfield', fieldLabel: 'PhoneLink', name: 'PhoneLink' },
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id' },
        { xtype: 'hiddenfield', fieldLabel: '是否删除', name: 'IsDeleted' },
        { xtype: 'hiddenfield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date() },
        { xtype: 'hiddenfield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date() },
    ]
});
