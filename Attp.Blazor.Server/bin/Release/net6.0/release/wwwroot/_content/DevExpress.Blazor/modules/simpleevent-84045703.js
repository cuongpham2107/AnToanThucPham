class s{constructor(){this.action=null,this.handle=null}execute(s){this.cancel(),this.action=s,this.handle=setTimeout((()=>{var s;null===(s=this.action)||void 0===s||s.call(this),this.handle=null,this.action=null}),0)}cancel(){this.handle&&(clearTimeout(this.handle),this.action=null,this.handle=null)}}class h{constructor(){this.handlers=[],this.raise=s=>{this.handlers.slice(0).forEach((h=>h(s)))}}subscribe(s){this.handlers.push(s)}unsubscribe(s){this.handlers=this.handlers.filter((h=>h!==s))}}class i{constructor(){this.handlers=[]}subscribe(s){this.handlers.push(s)}unsubscribe(s){this.handlers=this.handlers.filter((h=>h!==s))}raise(s,h){this.handlers.slice(0).forEach((i=>i(s,h)))}}export{s as D,h as S,i as a};
