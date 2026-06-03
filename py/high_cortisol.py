import multiprocessing
import time
import math

def cpu_worker(run_event, counter):
    local_count = 0
    while run_event.is_set():
        for i in range(1, 1000):
            math.sqrt(i) * math.sin(i)
        local_count += 1000
    counter.value += local_count

if __name__ == "__main__":
    num_cores = multiprocessing.cpu_count()
    print(f"Detected CPU cores: {num_cores}")

    benchmark_duration = 10
    print(f"Running CPU benchmark for {benchmark_duration} seconds...")

    counters = [multiprocessing.Value('i', 0) for _ in range(num_cores)]
    run_event = multiprocessing.Event()
    run_event.set()

    processes = []
    for i in range(num_cores):
        p = multiprocessing.Process(target=cpu_worker, args=(run_event, counters[i]))
        p.start()
        processes.append(p)

    time.sleep(benchmark_duration)
    run_event.clear()

    for p in processes:
        p.join()

    total_ops = sum(c.value for c in counters)
    score = total_ops // 1000

    print("\n--- Benchmark Results ---")
    for i, c in enumerate(counters):
        print(f"Core {i+1}: {c.value} operations")

    print(f"\nTotal CPU Score: {score}")