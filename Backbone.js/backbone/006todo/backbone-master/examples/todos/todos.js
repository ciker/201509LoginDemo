// An example Backbone application contributed by
// [Jérôme Gravel-Niquet](http://jgn.me/). This demo uses a simple
// [LocalStorage adapter](backbone.localstorage.html)
// to persist Backbone models within your browser.

// Load the application once the DOM is ready, using `jQuery.ready`:
$(function(){

  // Todo Model
  // ----------

  // Our basic **Todo** model has `title`, `order`, and `done` attributes.
  var Todo = Backbone.Model.extend({

    // 默认配置
    defaults: function() {
      return {
        title: "empty todo...",
        //序号
        order: Todos.nextOrder(),
        done: false
      };
    },

    // toggle方法
    toggle: function() {
    	//切换done的状态并保存
      this.save({done: !this.get("done")});
    }

  });

  // Todo Collection
  // ---------------

  // The collection of todos is backed by *localStorage* instead of a remote
  // server.
  var TodoList = Backbone.Collection.extend({

    // 对应Todo这个model
    model: Todo,

    // 存储到本地
    localStorage: new Backbone.LocalStorage("todos-backbone"),

    // 返回所有done状态为true的值
    done: function() {
      return this.where({done: true});
    },

    // 返回所有done状态为false的值
    remaining: function() {
      return this.where({done: false});
    },

    // 返回下一序列号
    nextOrder: function() {
      if (!this.length) return 1;
      return this.last().get('order') + 1;
    },

    // 按照指定属性进行排序
    comparator: 'order'

  });

  // Create our global collection of **Todos**.
  var Todos = new TodoList;

  // Todo Item View
  // --------------

  // The DOM element for a todo item...
  var TodoView = Backbone.View.extend({

    //为视图指定根元素
    tagName:  "li",

    // 使用模板可以更加方便的访问实体数据
    template: _.template($('#item-template').html()),

    // 绑定事件
    events: {
      "click .toggle"   : "toggleDone",
      "dblclick .view"  : "edit",
      "click a.destroy" : "clear",
      "keypress .edit"  : "updateOnEnter",
      "blur .edit"      : "close"
    },

    // The TodoView listens for changes to its model, re-rendering. Since there's
    // a one-to-one correspondence between a **Todo** and a **TodoView** in this
    // app, we set a direct reference on the model for convenience.
    initialize: function() {
    	//构造函数
    	//使view可以监听model上的特定事件
      this.listenTo(this.model, 'change', this.render);//如果model改变，重新渲染
      this.listenTo(this.model, 'destroy', this.remove);//如果model移除，视图也移除
    },

    // Re-render the titles of the todo item.
    render: function() {
      this.$el.html(this.template(this.model.toJSON()));
      //通过toggleClass使model和view的done状态保持同步
      this.$el.toggleClass('done', this.model.get('done'));
      //将input属性和编辑框对应起来
      this.input = this.$('.edit');
      //通过return this 开启链式调用
      return this;
    },

    // 切换done的状态
    toggleDone: function() {
      this.model.toggle();
    },

    // 编辑对应操作
    edit: function() {
      this.$el.addClass("editing");
      this.input.focus();
    },

    // 编辑结束触发事件
    close: function() {
      var value = this.input.val();
      if (!value) {
      	//如果没有值，则从model中删除所有属性
        this.clear();
      } else {
      	//保存改变值
        this.model.save({title: value});
        this.$el.removeClass("editing");
      }
    },

    //点击回车键后触发事件
    updateOnEnter: function(e) {
      if (e.keyCode == 13) this.close();
    },

    // 点击叉叉后触发事件
    clear: function() {
      this.model.destroy();
    }

  });

  // The Application
  // ---------------

  // Our overall **AppView** is the top-level piece of UI.
  var AppView = Backbone.View.extend({

//这里不是生成一个新的元素，而是指向一个页面中已经存在的元素
    el: $("#todoapp"),

    // Our template for the line of statistics at the bottom of the app.
    statsTemplate: _.template($('#stats-template').html()),

    // Delegated events for creating new items, and clearing completed ones.
    events: {
      "keypress #new-todo":  "createOnEnter",
      "click #clear-completed": "clearCompleted",
      "click #toggle-all": "toggleAllComplete"
    },

    // At initialization we bind to the relevant events on the `Todos`
    // collection, when items are added or changed. Kick things off by
    // loading any preexisting todos that might be saved in *localStorage*.
    initialize: function() {
	  //将input属性和输入框对应起来
      this.input = this.$("#new-todo");
      this.allCheckbox = this.$("#toggle-all")[0];
	  //监听另一对象Todos的事件
      this.listenTo(Todos, 'add', this.addOne);//当一个模型加入到集合时触发
      this.listenTo(Todos, 'reset', this.addAll);//集合内容全部被替换时触发
      this.listenTo(Todos, 'all', this.render);//所有事件发生都会触发这个特别事件

      this.footer = this.$('footer');
      this.main = $('#main');
	  //惰性加载数据,调用完后就会调用触发reset方法
      Todos.fetch();
    },

    // Re-rendering the App just means refreshing the statistics -- the rest
    // of the app doesn't change.
    render: function() {
      var done = Todos.done().length;//获取集合中所有done状态为true的数组的长度
      var remaining = Todos.remaining().length;//获取集合中所有done状态为false的数组的长度

      if (Todos.length) {
        this.main.show();
        this.footer.show();
        //将获得的变量丢入模板中
        this.footer.html(this.statsTemplate({done: done, remaining: remaining}));
      } else {
        this.main.hide();
        this.footer.hide();
      }

      this.allCheckbox.checked = !remaining;
    },

    // Add a single todo item to the list by creating a view for it, and
    // appending its element to the `<ul>`.
    addOne: function(todo) {
      var view = new TodoView({model: todo});
      //将构建的列表项view加入列表中
      this.$("#todo-list").append(view.render().el);
    },

    // 将所有数据渲染到页面，页面加载时用到
    addAll: function() {
      Todos.each(this.addOne, this);
    },

    // If you hit return in the main input field, create new **Todo** model,
    // persisting it to *localStorage*.
    //创建新任务回车触发事件
    createOnEnter: function(e) {
      if (e.keyCode != 13) return;
      if (!this.input.val()) return;

      Todos.create({title: this.input.val()});
      this.input.val('');
    },

    // Clear all done todo items, destroying their models.
    clearCompleted: function() {
    	//为集合中每一个元素执行指定方法
      _.invoke(Todos.done(), 'destroy');
      return false;
    },

    toggleAllComplete: function () {
      var done = this.allCheckbox.checked;
      Todos.each(function (todo) { todo.save({'done': done}); });
    }

  });

  // 这里就是程序的入口
  var App = new AppView;

});
