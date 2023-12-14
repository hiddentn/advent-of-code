from math import log10, ceil

with open('input.txt', encoding = 'utf-8') as f:
    dat = [x.strip('\n') for x in f.readlines()]

def apply_maps(maps, seed):
    pre_map = seed
    for m in maps:
        for ds, ss, rl in m:
            if ss <= pre_map < ss + rl:
                pre_map = ds + (pre_map - ss)
                break
    return pre_map

def parse(seed_ranges = False):
    seeds = [int(x) for x in dat[0].split(': ')[1].split(' ')]

    if seed_ranges:
        seeds = [(seeds[2 * i], seeds[2 * i + 1]) for i in range(len(seeds) // 2)]

    maps = []
    curr_map = []
    for d in dat[3:]:
        if d == '':
            continue
        if ':' in d:
            maps += [curr_map]
            curr_map = []
        else:
            curr_map += [tuple(int(x) for x in d.split(' '))]

    maps += [curr_map]

    return seeds, maps

def part1(output = True):
    seeds, maps = parse(seed_ranges = False)
    locs = {apply_maps(maps, s): s for s in seeds}
    min_loc = min(locs.keys())

    if output:
        for lk, lv in locs.items():
            if lk == min_loc:
                print("", end='')
            print(f'Seed {lv:<10d} maps to location {lk}')
            if lk == min_loc:
                print("", end='')

    return min_loc

def part2(output = True):
    import re

    def ints(s):
        return [int(m) for m in re.findall(r'-?[\d]+', s)]

    seed_names, map_sets = dat[0], dat[1:]
    seeds = ints(seed_names)
    min_location = 99999999999999999

    for seed in seeds:
        location = seed
        for map_set in map_sets:
            for line in map_set.split('\n')[1:]:
                dst_start, src_start, rng = ints(line)
                if location in range(src_start, src_start + rng):
                    location = dst_start + (location - src_start)
                    break
        min_location = min(min_location, location)

    return min_location

if __name__ == '__main__':
    print('Part 1:', part1(True))
    print('Part 2:', part2(True))