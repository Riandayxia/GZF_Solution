/*
菜单类型
*/
Ext.define('Base.Menu.Tree', {
    extend: 'Ext.user.NTree',
    xtype: 'base_menu_tree',
    rootVisible: false,
    //columnLines: false,
    rowLines: false,
    controllerName: 'Menu',
    rootValue: 'data',
    dataUrl: appBaseUrl + 'Menu/GetRoots'
});
