"""
Main app used to run the metric rag controllers.
"""

from controllers.metricrag_controller import MetricRagController
import sys
import os
import time

project_root = os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))
sys.path.insert(0, project_root)


if __name__ == "__main__":
    t0 = time.time()
    metric_rag_controller = MetricRagController()
    metric_rag_controller.run_metric_rag_calculation()
    print('Time to complete: ', time.time() - t0)