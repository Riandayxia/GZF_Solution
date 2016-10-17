Ext.define('UX.WorkFlow.Designer', {
    extend: 'Ext.container.Container',
    alias: 'widget.wfdesigner',
    requires: [
        'UX.WorkFlow.frame.draw-min'
    ],
    style: {
        'background-color': 'white;',
        'border-top': '1px solid #ccc'
    },
    layout: 'border',
    items: [
        {
            title: 'Center Region',
            region: 'center',     // center region is required, no width/height specified
            xtype: 'panel',
            layout: 'fit',
            margins: '5 5 0 0'
        }],
    height: '100%',
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
        //me.WF = Raphael(me.getId(), '100%', me.height);
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.WF = Raphael(me.getId(), '100%', me.height);
    }
});