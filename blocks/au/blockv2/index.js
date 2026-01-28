import { defineComponent as s, openBlock as a, createElementBlock as _, createElementVNode as c, toDisplayString as n } from "vue";
const i = { class: "remote-block" }, d = { style: { color: "red" } }, p = /* @__PURE__ */ s({
  __name: "Block",
  props: {
    title: {}
  },
  setup(t) {
    return (o, e) => (a(), _("div", i, [
      c("h2", d, n(t.title), 1),
      c("h3", null, n(t.title), 1)
    ]));
  }
}), m = (t, o) => {
  const e = t.__vccOpts || t;
  for (const [l, r] of o)
    e[l] = r;
  return e;
}, k = /* @__PURE__ */ m(p, [["__scopeId", "data-v-ca94c19d"]]);
export {
  k as default
};
