using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;

namespace Microsoft.Xna.Framework.Timing;

public partial class WaitableTimer : WaitHandle
{
	[LibraryImport("kernel32.dll", EntryPoint = "CreateWaitableTimerExW")]
	private static partial SafeWaitHandle CreateWaitableTimerEx(IntPtr lpTimerAttributes, [MarshalAs(UnmanagedType.LPWStr)] string lpTimerName, uint dwFlags, uint dwDesiredAccess);

	[LibraryImport("kernel32.dll",  SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool SetWaitableTimer(SafeWaitHandle hTimer, ref long pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr lpArgToCompletionRoutine, [MarshalAs(UnmanagedType.Bool)] bool fResume);

	private const uint CREATE_WAITABLE_TIMER_HIGH_RESOLUTION = 0x00000002;
	private const uint TIMER_ALL_ACCESS = 0x1F0003;

	public WaitableTimer(bool manualReset = false, string timerName = null)
	{
		SafeWaitHandle handle = CreateWaitableTimerEx(
			IntPtr.Zero,
			timerName,
			CREATE_WAITABLE_TIMER_HIGH_RESOLUTION | (manualReset ? 0x00000001u : 0u),
			TIMER_ALL_ACCESS
		);

		if (handle == null || handle.IsInvalid)
		{
			throw new Win32Exception();
		}

		SafeWaitHandle = handle;
	}

	public void Set(long dueTime)
	{
		if (!SetWaitableTimer(SafeWaitHandle, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, false))
		{
			throw new Win32Exception();
		}
	}

	// Negative values indicate relative time.
	// See https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-setwaitabletimer#parameters
	public void SetRelativeTimeSpan(TimeSpan timeSpan)
	{
		Set(-timeSpan.Ticks);
	}
}
