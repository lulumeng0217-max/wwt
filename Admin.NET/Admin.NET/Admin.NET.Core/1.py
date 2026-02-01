#!/usr/bin/env python3
import os
import sys

# 要清理的注释块起始文本（按你的实际文本修改）
HEADER_START = "// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。"

# 头部注释块的可接受行（注意顺序应与实际文件一致）
HEADER_LINES = [
    HEADER_START,
    "//",
    "// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。",
    "//",
    "// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！",
]

def remove_header_block(lines):
    # 寻找以 HEADER_START 开头的头部注释块
    idx = None
    for i, line in enumerate(lines):
        if line.strip().startswith(HEADER_START):
            idx = i
            break
    if idx is None:
        return lines  # 未发现目标头部，原样返回

    # 从 idx 开始，持续删除直到遇到非注释行或非预期注释行
    end = idx
    n = len(lines)
    while end < n:
        t = lines[end].strip()
        if t.startswith("//"):
            # 仅允许在 HEADER_LINES 内的注释，或仅为 "//" 的分隔线
            if t in [l.strip() for l in HEADER_LINES] or t == "//":
                end += 1
                continue
            else:
                break
        else:
            break

    # 新的文件内容：前 idx 行 + 之后的 rest
    new_lines = lines[:idx] + lines[end:]
    # 去掉前导空行，确保第一行为代码
    while new_lines and new_lines[0].strip() == "":
        new_lines.pop(0)
    return new_lines

def process_file(path):
    try:
        with open(path, "r", encoding="utf-8-sig") as f:
            lines = f.readlines()
    except Exception as e:
        print(f"Skip {path}: {e}", file=sys.stderr)
        return
    new_lines = remove_header_block(lines)
    if new_lines != lines:
        with open(path, "w", encoding="utf-8-sig") as f:
            f.writelines(new_lines)

def walk(root_dir):
    exts = {".cs", ".ts", ".tsx", ".js", ".vue", ".json"}
    for dirpath, _, filenames in os.walk(root_dir):
        for name in filenames:
            if os.path.splitext(name)[1].lower() in exts:
                process_file(os.path.join(dirpath, name))

if __name__ == "__main__":
    ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__)))
    walk(ROOT)
