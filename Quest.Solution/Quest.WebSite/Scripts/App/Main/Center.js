/*
内容
*/
Ext.define('Main.Center', {
    extend: 'Ext.tab.Panel',
    xtype: 'main_center',
    border:'1',
    activeTab: 0,
    items: {
        title: '首页',
        html: 'The first tab\'s content. Others may be added dynamically'
    }
});
