/*
工程管理布局
*/
Ext.define('Compiler.HelloWorld.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'main_layout',
    layout: 'border',
    requires: ['Compiler.HelloWorld.Auto.Grid'],
    items: [{
        region: 'center', collapsible: true, split: true, header: false
    }, {
        region: 'south', collapsible: true, split: true, header: false, height: '50%'
    }],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});
