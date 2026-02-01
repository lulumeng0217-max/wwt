import { defineComponent as r, openBlock as s, createElementBlock as l, createElementVNode as a, toDisplayString as _ } from "vue";
const p = { class: "remote-block" }, i = /* @__PURE__ */ r({
  __name: "Block",
  props: {
    title: {}
  },
  setup(t) {
    return (o, e) => (s(), l("div", p, [
      a("h3", null, _(t.title), 1)
    ]));
  }
}), m = (t, o) => {
  const e = t.__vccOpts || t;
  for (const [c, n] of o)
    e[c] = n;
  return e;
}, f = /* @__PURE__ */ m(i, [["__scopeId", "data-v-c025b4a4"]]);
export {
  f as default
};
