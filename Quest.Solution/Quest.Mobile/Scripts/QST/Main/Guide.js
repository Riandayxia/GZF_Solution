//欢迎页
Ext.define('QST.Main.Guide', {
    extend: 'Ext.Carousel',
    alternateClassName: 'guide',
    xtype: 'guide',
    requires: ['QST.Main.Layout'],
    config: {
        items: [{
            html: '<img  class="middle_img" fire="onDelete" src="resources/images/Guide1.png">'
        }, {
            html: '<img  class="middle_img" fire="onDelete" src="resources/images/Guide2.png">'
        }, {
            html: '<img  class="middle_img" fire="onDelete" src="resources/images/Guide3.png">'
        }]
    },
    initialize: function () {
        var me = this;
        //监听幻灯片旋转事件
        me.on({
            scope: me,
            painted: 'onPainted'
        });
    },
    onPainted: function () {
        var me = this;
        me.element.on('dragend',
        function (e) {
            if (me.offset == 0) {
                //当幻灯片转到最后一页时
                if (me.getActiveIndex() == me.getMaxItemIndex()) {
                    ////触发自定义事件
                    //me.fireEvent('showMain');
                    setTimeout(function () { util.redirectTo('QST.Main.Layout'); }, 0)
                }
            }
        });
    }
});