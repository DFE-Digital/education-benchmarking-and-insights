"""
The metricrag controller
"""

from config.config import Config

from helpers.sp_my_comparison_pupils_mix import SpMyComparisonPupilsMixHelper
from helpers.sp_my_comparison_pupils_ms import SpMyComparisonPupilsMsHelper
from helpers.sp_my_comparison_pupils import SpMyComparisonPupilsAcHelper
from helpers.sp_my_comparison_area_mix import SpMyComparisonAreaMixHelper
from helpers.sp_my_comparison_area_ms import SpMyComparisonAreaMsHelper
from helpers.sp_my_comparison_area import SpMyComparisonAreaHelper

class MetricRagController():
    
    def __init__(self):
        self.config = Config()
        self.sp_my_comparison_pupils_mix = SpMyComparisonPupilsMixHelper()
        self.sp_my_comparison_pupils_ms = SpMyComparisonPupilsMsHelper()
        self.sp_my_comparison_pupils = SpMyComparisonPupilsAcHelper()
        self.sp_my_comparison_area_mix = SpMyComparisonAreaMixHelper()
        self.sp_my_comparison_area_ms = SpMyComparisonAreaMsHelper()
        self.sp_my_comparison_area = SpMyComparisonAreaHelper()

    def run_metric_rag_calculation(self):
        self.sp_my_comparison_pupils_mix.run_procedure()
        self.sp_my_comparison_pupils_ms.run_procedure()
        self.sp_my_comparison_pupils.run_procedure()
        self.sp_my_comparison_area_mix.run_procedure()
        self.sp_my_comparison_area_ms.run_procedure()
        self.sp_my_comparison_area.run_procedure()