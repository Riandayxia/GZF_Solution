/*
菜单
*/
Ext.define('Main.Menu', {
    extend: 'Ext.user.NTree',
    xtype: 'main_menu',
    rootVisible: false,
    columnLines: false,
    rowLines: false,
    controllerName: 'Menu',
    title: '菜单',
    rootValue: 'data',
    dataUrl: appBaseUrl + 'Menu/GetTree',
    listeners: {
        itemclick: function (e, record) {
            if (record.data.leaf) {
                this.openTab(e, record);
            }
        }
    },
    openTab: function (e, record) {
        var me = this;
        var mainTab = me.up('main_layout').down('tabpanel');
        var data = record.data.Tobject;
        var tabId = 'tab' + data.Id;
        var _tab = mainTab.getComponent(tabId);

        if (!_tab) {
            mainTab.setLoading('Loading...');
            var link = data.MenuLink;
            _tab = Ext.create('Ext.panel.Panel', {
                closable: true,
                id: tabId,
                title: data.Name,
                layout: 'fit',
                autoScroll: true,
                border: false,
                items: Ext.create(link, { Menu: data })
            });
            mainTab.add(_tab);
            mainTab.setLoading(false);
        }
        mainTab.setActiveTab(_tab);
    }
});
