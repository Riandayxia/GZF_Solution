Ext.define('QST.Property.Convenience.Middle', {
    extend: 'Ext.Container',
    xtype: 'property_convenience_middle',
    alternateClassName: 'property_convenience_middle',
    config: {
        layout: 'vbox',
        //style: 'border:1px solid #CCC',
        defaults: {
            layout: 'hbox',
            defaults: {
                flex: 1,
                xtype: 'label',
                iconAlign: 'top',
                style: 'margin-left:.5em;',
            }
        },
        items: [
           {

               html: '<div class="conveniencediv"><div class="conveniencespan">热门</div> <div class="conveniencesimg">更多<img class="personalArrow" fire="onDelete" src="resources/images/Arrow.png"></div></div>'
           }
        ]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);

    }
})